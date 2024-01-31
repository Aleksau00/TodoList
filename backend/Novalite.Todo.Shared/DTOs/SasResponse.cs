using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novalite.Todo.Shared.DTOs
{
    public class SasResponse
    {
        public required string Sas { get; set; }
        public required string AttachmentId { get; set; }
    }
}
