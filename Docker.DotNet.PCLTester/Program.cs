using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.DotNet.PCLTester
{
    class Program
    {
        static void Main(string[] args)
        {
            // This assumes you are running Boot2Docker and have retrieved the IP for the instance
            DockerClient client = new DockerClientConfiguration(new Uri("http://192.168.59.103:2375"))
     .CreateClient();
            client.Miscellaneous.PingAsync();
            var images = client.Images.ListImagesAsync(new Models.ListImagesParameters()).Result;
            var containers = client.Containers.ListContainersAsync(new Models.ListContainersParameters()).Result;
            images.ToList().ForEach(x => {
                Console.WriteLine(x.Id);
            });
            containers.ToList().ForEach(x =>
            {
                Console.WriteLine("Container {0}", x.Names[0]);
                Console.WriteLine(client.Containers.InspectContainerAsync(x.Id).Result);
            });

            Console.ReadLine();
        }
    }
}
