using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class MessageDTO
    {

        public MessageDTO()
        {

        }

        public MessageDTO(params string[] messages)
        {
            Messages = messages;
        }
        public IList<string> Messages { get; set; } = new List<string>();
    }
}