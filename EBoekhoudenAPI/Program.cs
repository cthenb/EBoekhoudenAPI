using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBoekhoudenAPI.EbApi;

namespace EBoekhoudenAPI
{
    public class Mutation : cMutatie
    {

    }

    public class Program
    {
        public static void Main (string[] args)
        {

        }

        public static void SendMutation(
            string username,
            string securityCode1,
            string securityCode2,
            Mutation mutation
            )
        {
            using (soapAppSoapClient client = new soapAppSoapClient())
            {
                var result = client.OpenSession(username, securityCode1, securityCode2);
                var sessionId = result.SessionID;

                if (!string.IsNullOrEmpty(result.ErrorMsg.LastErrorCode))
                    throw new Exception("Cannot connect to E-Boekhouden");

                var response = client.AddMutatie(sessionId, securityCode2, mutation);

                if (response.ErrorMsg.LastErrorCode != null)
                    Console.WriteLine(response.ErrorMsg.LastErrorDescription);
            }
        }
    }
}
