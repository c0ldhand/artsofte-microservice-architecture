using CoreLib.DistributedSemaphore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/semaphore")]


    public class SemaphoreController : ControllerBase
    {
        private readonly IDistributedSemaphore _semaphore;

        public SemaphoreController(IDistributedSemaphore semaphore)
        {
            _semaphore = semaphore;
        }
        [HttpGet("run")]
        public async Task<IActionResult> Run()
        {
            var key = "my-semaphore:demo";
            var max = 3;
            var leaseTtl = TimeSpan.FromSeconds(10);
            var lease = await _semaphore.AcquireAsync(key, max, TimeSpan.FromSeconds(5), leaseTtl);

            if (lease == null) return StatusCode(StatusCodes.Status429TooManyRequests, "Could not acquire semaphore");
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                return Ok(new { message = "worked", token = lease.Token });
            }
            finally
            {
                await lease.ReleaseAsync();
            }


        }


    }
}
