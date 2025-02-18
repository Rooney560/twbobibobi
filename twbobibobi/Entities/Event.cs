using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace twbobibobi.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public int ResourceCount {  get; set; }
        public long FirstResourceId {  get; set; }
        public string FirstResourceImageType {  get; set; }
    }
}