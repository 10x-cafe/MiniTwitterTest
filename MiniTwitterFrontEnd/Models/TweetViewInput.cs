using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MiniTwitterFrontEnd.Models
{
    public class TweetViewInput
    {
        [Required]
        public IFormFile UserFile { set; get; }

        [Required]
        public IFormFile TweetFile { set; get; }
    }
}
