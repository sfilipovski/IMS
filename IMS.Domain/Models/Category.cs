using IMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IMS.Domain.Models;

public class Category : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string CategoryName { get; set; }
    public string CategoryType { get; set; }
    [MaxLength(255)]
    public string CategoryDescription { get; set; }
   /* [JsonIgnore]
    public virtual ICollection<Product>? CategoryProducts { get; set; }*/
}
