using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaStar.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; } 
        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        public string? ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public User? Receiver { get; set; }

        [Required]
        public string Content { get; set; } 

        public DateTime SentAt { get; set; } = DateTime.Now;
    }
}