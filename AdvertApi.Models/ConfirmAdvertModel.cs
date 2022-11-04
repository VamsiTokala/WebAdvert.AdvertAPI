using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewAdvertApi.Models
{
    public class ConfirmAdvertModel
    {
        public string Id { get; set; }// ID is returned with the API that creates the records in the database.
        public AdvertStatus Status { get; set; }
    }
}
