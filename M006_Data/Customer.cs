﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M006_Data;

[Index("City", Name = "City")]
[Index("CompanyName", Name = "CompanyName")]
[Index("PostalCode", Name = "PostalCode")]
[Index("Region", Name = "Region")]
public partial class Customer
{
    [Key]
    [Column("CustomerID")]
    [StringLength(5, MinimumLength = 5, ErrorMessage ="CustomerId muss genau 5 Zeichen haben!")]
    public string CustomerId { get; set; }

    [Required]
    [StringLength(40, MinimumLength = 1, ErrorMessage ="Der Firmenname muss zwischen 1 und 40 Zeichen haben!")]
    public string CompanyName { get; set; }

    [StringLength(30, MinimumLength = 1, ErrorMessage = "Der Ansprechpartner muss vorhanden sein!")]
    public string ContactName { get; set; }

    [StringLength(30)]
    public string ContactTitle { get; set; }

    [StringLength(60)]
    public string Address { get; set; }

    [StringLength(15)]
    public string City { get; set; }

    [StringLength(15)]
    public string Region { get; set; }

    [StringLength(10)]
    public string PostalCode { get; set; }

    [StringLength(15, MinimumLength = 1, ErrorMessage = "Das Land muss vorhanden sein!")]
    public string Country { get; set; }

    [StringLength(24)]
    public string Phone { get; set; }

    [StringLength(24)]
    public string Fax { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Customers")]
    public virtual ICollection<CustomerDemographic> CustomerTypes { get; set; } = new List<CustomerDemographic>();
}