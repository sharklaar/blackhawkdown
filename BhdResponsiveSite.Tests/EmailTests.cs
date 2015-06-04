using System.Collections.Generic;
using System.Linq;
using BhdResponsiveSite.Models;
using NUnit.Framework;

namespace BhdResponsiveSite.Tests
{
    [TestFixture]
    public class EmailTests
    {
        [Test]
        public void SendingASingleEmailWorks()
        {
            var emailList = new List<string>
            {
                "marc@blackhawkdown.org.uk"
            };

            var emailHelper = new Library.Email();

            var resultsList = new List<string>();

            foreach (var email in emailList)
            {
                var emailModel = new EmailOnlyModel {Email = email};

                var result = emailHelper.SendAutoResponse(emailModel, "FreeTracks");

                resultsList.Add(result);
            }

            Assert.That(resultsList.Where(x => x.Equals("success")).ToList().Count == emailList.Count);
        }
    }
}
