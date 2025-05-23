using GrpcService;
using Grpc.Net.Client;
using System.Runtime.InteropServices.Marshalling;
namespace ProjectB.Services
{
    public class GrpcClient
    {
        public async Task<HelloReply> GetGrpcResponse(HelloRequest heloRequest) {
            using var channel= GrpcChannel.ForAddress("http://localhost:5194");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(heloRequest);
            return reply;
        }

    }
        
}
