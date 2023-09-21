using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Response
{
    public class MessageResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public MessageResponseDTO(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public MessageResponseDTO(bool success)
        {
            Success = success;
        }
    }
}
