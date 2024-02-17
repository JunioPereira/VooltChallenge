using Microsoft.AspNetCore.Mvc;
using VooltModels;
using VooltServices.Interfaces;

namespace VooltChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VooltController : ControllerBase
    {
        ILogger<VooltController> _logger { get; }
        IReadWriteFile iReadWriteFile { get; }

        public VooltController(IReadWriteFile readWriteFile, ILogger<VooltController> logger)
        {
            iReadWriteFile = readWriteFile;
            _logger = logger;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get([FromRoute] string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key) || key.Contains(" "))
                {
                    return UnprocessableEntity("Invalid key");
                }

                var result = await iReadWriteFile.ReadAsync<ContentsData>(key);

                if(result.Item1)
                    return Ok(result.Item2);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> Post([FromRoute]string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key) || key.Contains(" ")) 
                {
                    return UnprocessableEntity("Invalid key");
                }

                List<Block_1> lstBlock_1 = new List<Block_1>(1);
                List<Block_2> lstBlock_2 = new List<Block_2>(1);
                List<Block_3> lstBlock_3 = new List<Block_3>(1);

                lstBlock_1.Add(new Block_1());
                lstBlock_2.Add(new Block_2());
                lstBlock_3.Add(new Block_3());

                var value = new ContentsData(lstBlock_1, lstBlock_2, lstBlock_3);

                var result = await iReadWriteFile.CreateAsync(key, value);

                if(result)
                    return Ok();
                else
                    return UnprocessableEntity("Some error occurred!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromRoute] string key, [FromBody] ContentsData body)
        {
            try
            {
                if (string.IsNullOrEmpty(key) || key.Contains(" "))
                {
                    return UnprocessableEntity("Invalid key");
                }

                var result = await iReadWriteFile.UpdateAsync(key, body);

                if (result)
                    return Created();
                else
                    return UnprocessableEntity("Some error occurred!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key) || key.Contains(" "))
                {
                    return UnprocessableEntity("Invalid key");
                }

                var result = await iReadWriteFile.DeleteAsync<ContentsData>(key);

                if (result)
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
        }
    }
}
