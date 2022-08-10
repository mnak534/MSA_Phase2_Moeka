using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSAbackend_phase2_cat;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MSA.Phase2.AmazingApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CatController : ControllerBase
    {

        private readonly HttpClient _client;
        /// <summary />
        public CatController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("cat");
        }
        /// <summary>
        /// Gets a random image of a cat
        /// </summary>
        /// <returns>A random image of a cat</returns>
        [HttpGet]
        [Route("/GetRandomCatImage")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetRandomCatImage()
        {
            var res = await _client.GetAsync("/v1/images/search");
            var content = await res.Content.ReadAsStringAsync();
            return Ok(content);
        }
        /// <summary>
        /// Gets an image of a cat by its ID
        /// </summary>
        /// <returns>An image of a cat specified by its ID</returns>
        [HttpGet]
        [Route("/GetCatImagebyID")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetCatImagebyID(string id)
        {
            var res = await _client.GetAsync("/v1/images/" + id);
            var content = await res.Content.ReadAsStringAsync();
            return Ok(content);
        }
        /// <summary>
        /// Gets a vote its vote ID
        /// </summary>
        /// <returns>A vote specified by its vote ID</returns>
        [HttpGet]
        [Route("/GetVote")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetVote(string voteID)
        {
            var res = await _client.GetAsync("/v1/votes/" + voteID);
            var content = await res.Content.ReadAsStringAsync();
            return Ok(content);
        }

        /// <summary>
        /// Posts a vote for an image of a cat (value is a vote and can be 1 or 0)
        /// </summary>
        /// <returns>A vote id and if the vote was success or not</returns>
        [HttpPost]
        [Route("/PostVote")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> PostVote(CatVotes vote)
        {
            var json = JsonConvert.SerializeObject(vote);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var res = await _client.PostAsync("/v1/votes", data);
            var content = await res.Content.ReadAsStringAsync();

            return StatusCode(StatusCodes.Status201Created, content);
        }

        /// <summary>
        /// Updates the vote value if the vote exsists already otherwise creates a new vote
        /// </summary>
        /// <returns>A 201 Created Response></returns>
        [HttpPut]
        [Route("/PutVote")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> PutVoteAsync(string voteID, CatVotes vote)
        {
            
            if (vote == null) 
            { return BadRequest("please enter the vote"); }

            await _client.DeleteAsync("/v1/votes/" + voteID);

            var json = JsonConvert.SerializeObject(vote);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _client.PostAsync("/v1/votes", data);
            var content = await response.Content.ReadAsStringAsync();

            return StatusCode(StatusCodes.Status201Created, content);
        }

        /// <summary>
        /// Deletes a vote
        /// </summary>
        /// <returns>A 204 No Content Response</returns>
        [HttpDelete]
        [Route("/DeleteVote")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteVote(string voteID)
        {
            HttpResponseMessage res = await _client.DeleteAsync("/v1/votes/" + voteID);
            if (!res.IsSuccessStatusCode)
            {
                return BadRequest(res.ToString());
            }
            var result = await res.Content.ReadAsStringAsync();
            return StatusCode(StatusCodes.Status204NoContent, result);
        }
    }
}