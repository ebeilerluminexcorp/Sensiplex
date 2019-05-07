using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luminex.Models.Implementations;
using Luminex.Models.Interfaces;
using Luminex.Spartan.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace SensiplexAPI.Controllers
{
    /// <summary>
    ///This is for create and get Test Panels
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestPanelsController : ControllerBase
    {
        /// <summary>
        /// Get All Test Panels 
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAllPanels()
        {
            return  PanelHelper.RetrieveAllPanelIds();
        }
        /// <summary>
        /// Get Panel by Panelname
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public IPanelData GetAllPanelByID(string id)
        {
            return PanelHelper.GetPanelByName(id);
        }

        // POST api/values
        [HttpPost]
        public void CreatePanel([FromBody] PanelData panelData)
        {
            PanelHelper.SavePanel(panelData);
        }

        // PUT api/values/5
        [HttpPut("{PanelData}")]
        public void UpdatePanel([FromBody] PanelData panelData)
        {
            PanelHelper.SavePanel(panelData);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void DeletePanel(int id)
        {
        }
    }
}
