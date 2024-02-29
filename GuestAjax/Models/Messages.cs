using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuestAjax.Models
{
    public class Messages
    {
        public int Id { get; set; }
      
        public string? message { get; set; }
        public int? mark { get; set; }
        //[Required(ErrorMessage = "Выберите дату")]
       
        public DateTime? Datetime { get; set; }
        [NotMapped]
     public string? date { get; set; }
        public Users? user { get; set; }
		[NotMapped]
        public int UserId { get; set; }
	}
}
