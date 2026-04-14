using Grpc.Core;
using Veeho.Grpc;

namespace Veeho.API.VideoGrpcService
{
    public class VideoGrpcService : VideoService.VideoServiceBase
    {
        private readonly IWebHostEnvironment _env;
        public VideoGrpcService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public override async Task<UploadVideoResponse> Upload(IAsyncStreamReader<UploadVideoRequest> requestStream, ServerCallContext context)
        {
            var uploadPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "videos");
            Directory.CreateDirectory(uploadPath);
            string fileName = null;
            string userId = null;
            string videoId = Guid.NewGuid().ToString();
            var fullFilePath = "";
            await using var memoryStream = new MemoryStream();
            await foreach (var message in requestStream.ReadAllAsync())
            {
                userId ??= message.UserId;
                fileName ??= message.FileName;
                fullFilePath = Path.Combine(uploadPath, $"{videoId}_{fileName}");
                await memoryStream.WriteAsync(message.ChunkData.ToByteArray());
                if(message.IsLastChunk)
                {
                    await File.WriteAllBytesAsync(fullFilePath, memoryStream.ToArray());
                }
            }
            return new UploadVideoResponse
            {
                Success = true,
                VideoId = videoId,
                Message = "video upload successfully"
            };
        }
        public override async Task Download(VideoRequest request, IServerStreamWriter<VideoChunk> responseStream, ServerCallContext context)
        {
            var filePath = Path.Combine(_env.WebRootPath ?? "wwwroot", "videos", request.FileName);
            if (!File.Exists(filePath))
                throw new RpcException(new Status(StatusCode.NotFound, "File not found."));
            const int bufferSize = 64 * 1024; //64 KB
            byte[] buffer = new byte[bufferSize];
            await using var fs = File.OpenRead(filePath);
            int bytesRead;
            while((bytesRead = await fs.ReadAsync(buffer,0, buffer.Length)) > 0)
            {
                var chunk = new VideoChunk()
                {
                    FileName = request.FileName,
                    Data = Google.Protobuf.ByteString.CopyFrom(buffer, 0, bytesRead)
                };
                await responseStream.WriteAsync(chunk);
            }
        }
    }
}

