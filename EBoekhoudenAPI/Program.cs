using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBoekhoudenAPI.EbApi;
using Newtonsoft.Json;

namespace EBoekhoudenAPI
{
    public class Program
    {
        public static void Main (string[] args)
        {
            var config = Config.Get();
            var json = File.ReadAllText(config.AbsolutePath);
            var mutations = JsonConvert.DeserializeObject<IEnumerable<cMutatie>>(json);

            if (SendMutations(config.Username, config.SecurityCode1, config.SecurityCode2, mutations))
            {
                File.WriteAllText(config.AbsolutePath, string.Empty);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        public static bool SendMutations(
            string username,
            string securityCode1,
            string securityCode2,
            IEnumerable<cMutatie> mutations
            )
        {
            bool withoutErrors = true;

            using (soapAppSoapClient client = new soapAppSoapClient())
            {
                var result = client.OpenSession(username, securityCode1, securityCode2);
                var sessionId = result.SessionID;

                if (!string.IsNullOrEmpty(result.ErrorMsg.LastErrorCode))
                    throw new Exception("Cannot connect to E-Boekhouden");

                foreach (var mutation in mutations)
                {
                    var response = client.AddMutatie(sessionId, securityCode2, mutation);

                    if (response.ErrorMsg.LastErrorCode != null)
                    {
                        Console.WriteLine(response.ErrorMsg.LastErrorDescription);
                        withoutErrors = false;
                    }
                }
            }

            return withoutErrors;
        }
    }
}
