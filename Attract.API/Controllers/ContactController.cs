﻿using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Contact;
using Attract.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService contactService;

        public ContactController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet("GetAllContacts")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllContacts()
        {
            var contacts=await contactService.GetAllConatcts();
            return Ok(contacts);
        }

        [HttpPost("AddContact")]
        public async Task<ActionResult<BaseCommandResponse>> AddContact(AddContactDTO addContactDTO)
        {
            var contacts = await contactService.AddConatct(addContactDTO);
            return Ok(contacts);
        }
    }
}
