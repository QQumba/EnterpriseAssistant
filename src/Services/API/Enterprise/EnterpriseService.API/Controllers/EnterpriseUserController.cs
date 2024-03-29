﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseAssistant.Application.Shared;
using EnterpriseAssistant.DataAccess;
using EnterpriseAssistant.DataAccess.Entities.Enums;
using EnterpriseAssistant.DataAccess.Extensions;
using EnterpriseService.API.Commands;
using EnterpriseService.API.Helpers;
using EnterpriseService.Contract.DataTransfer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace EnterpriseService.API.Controllers;

[ApiController]
[Route("api/enterprise/user")]
[Authorize(Policy = "EnterpriseUser")]
public class EnterpriseUserController : ControllerBase
{
    private readonly DbContextFactory _factory;
    private readonly IMediator _mediator;

    public EnterpriseUserController(DbContextFactory factory, IMediator mediator)
    {
        _factory = factory;
        _mediator = mediator;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all users of the enterprise")]
    public async Task<ActionResult<IEnumerable<EnterpriseUserDto>>> GetUsers()
    {
        var authContext = User.GetAuthContext();
        var readOnlyContext = _factory.CreateReadOnlyContext(authContext);
        var users = await readOnlyContext.EnterpriseUsers
            .Include(eu => eu.User)
            .Where(eu => eu.User.IsSoftDeleted == false)
            .Select(eu => new EnterpriseUserDto
            {
                Id = eu.UserId,
                Login = eu.Login,
                Email = eu.User.Email,
                FirstName = eu.User.FirstName,
                LastName = eu.User.LastName
            })
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("exists")]
    public async Task<ActionResult<bool>> IsUserExists([Required] [FromQuery] string login)
    {
        var enterpriseId = User.GetEnterpriseId();
        var result = await _mediator.Send(new CheckIfEnterpriseUserExists(enterpriseId!, login));
        return Ok(result);
    }

    [HttpGet("exists/by-email")]
    public async Task<ActionResult<bool>> IsUserWithEmailExists([Required] [FromQuery] string email)
    {
        var enterpriseId = User.GetEnterpriseId()!;
        var context = _factory.Create();
        var result = await context.EnterpriseUsers.IsUserWithEmailExists(enterpriseId, email);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<EnterpriseUserDto>>> SearchUsers([Required] [FromQuery] string query)
    {
        var authContext = User.GetAuthContext();
        var readOnlyContext = _factory.CreateReadOnlyContext(authContext);
        var isUserAdmin = await readOnlyContext.DepartmentUsers
            .AnyAsync(du =>
                du.EnterpriseId == authContext.EnterpriseId
                && du.UserId == authContext.UserId
                && du.DepartmentUserRole == DepartmentUserRole.Admin);
        if (isUserAdmin == false)
        {
            return Ok(Array.Empty<EnterpriseUserDto>());
        }

        var users = await readOnlyContext.EnterpriseUsers
            .Include(eu => eu.User)
            .Where(eu => eu.User.IsSoftDeleted == false &&
                         eu.Login.StartsWith(query) ||
                         eu.User.FirstName.ToLower().StartsWith(query) ||
                         (eu.User.LastName != null && eu.User.LastName.ToLower().StartsWith(query)))
            .Select(eu => new EnterpriseUserDto
            {
                Id = eu.UserId,
                Login = eu.Login,
                Email = eu.User.Email,
                FirstName = eu.User.FirstName,
                LastName = eu.User.LastName
            })
            .ToListAsync();

        return users;
    }
}