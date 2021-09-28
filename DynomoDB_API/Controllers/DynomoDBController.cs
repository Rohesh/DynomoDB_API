using DynomoDB.Libs.DynomoDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynomoDB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynomoDBController : ControllerBase
    {
        private readonly IDynomoDbOrder _dynomoDbOrder;
        private readonly IPutItem _putitem;

        public DynomoDBController (IDynomoDbOrder dynomoDbOrder,IPutItem putItem )
        {
            _dynomoDbOrder = dynomoDbOrder;
            _putitem = putItem;
        }

        [Route ("CreateTable")]
        public IActionResult CreateDynomoTable()
        {
            _dynomoDbOrder.CreateDynomoDbTable();
            return Ok(); 
        }

        [Route ("PutItems")]
        public IActionResult PutItem([FromQuery ] int OrderID, string replyDateTime)
        {
            _putitem.AddNewEntry(OrderID, replyDateTime);
            return Ok();
        }
    }
}
