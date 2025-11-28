using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using twbobibobi.FET.Dto;

namespace twbobibobi.FET.Processors
{
    // TODO: 其它宮廟的 Processor 範例
    public class BeigangOrderProcessor : ITempleOrderProcessor
    {

        public Task ProcessAsync(ApplicantDto applicant, List<PrayedPersonDto> prayedPersons)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> ProcessAsync(ApplicantDto applicant, 
            List<PrayedPersonDto> prayedPersons, 
            string tid, 
            string fetOrderNumber, 
            string kind, 
            string totalAmount,
            string itemsInfo,
            string OrderId)
        {
            throw new NotImplementedException();
        }

    }
}