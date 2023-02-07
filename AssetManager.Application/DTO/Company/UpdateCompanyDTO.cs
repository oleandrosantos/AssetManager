﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AssetManager.Application.DTO.Company;
public class UpdateCompanyDTO
{
    [Required]
    public int idCompany { get; set; }
    public string? CompanyName { get; set; }
    [MinLength(14), StringLength(14)]
    public string? Cnpj { get; set; }
    [DefaultValue(true)]
    public bool IsAtiva { get; set; }
}