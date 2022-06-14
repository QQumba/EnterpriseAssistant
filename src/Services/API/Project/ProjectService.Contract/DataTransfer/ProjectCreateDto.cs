﻿using DepartmentService.Contract.DataTransfer;

namespace ProjectService.Contract.DataTransfer;

public class ProjectCreateDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = String.Empty;
    public string EnterpriseId { get; set; } = null!;
    public DepartmentCreateDto DepartmentCreate { get; set; } = null!;
};