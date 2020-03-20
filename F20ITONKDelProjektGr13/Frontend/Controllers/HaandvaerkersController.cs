using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.Models;

namespace Frontend.Controllers
{
    public class HaandvaerkersController : Controller
    {
        //private readonly FrontendContext _context;
        private readonly HttpClient _client;

        public HaandvaerkersController(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("backend");
        }

        // GET: Haandvaerkers
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(
                "api/haandvaerkers");

            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            return View(await JsonSerializer.DeserializeAsync<IEnumerable<Haandvaerker>>(responseStream, new JsonSerializerOptions()));
        }

        // GET: Haandvaerkers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync("api/haandvaerkers/" + id);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var haandvaerker = await JsonSerializer.DeserializeAsync<Haandvaerker>(responseStream);

            if (haandvaerker == null)
            {
                return NotFound();
            }

            return View(haandvaerker);
        }

        // GET: Haandvaerkers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Haandvaerkers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HaandvaerkerId,HVAnsaettelsedato,HVEfternavn,HVFagomraade,HVFornavn")] Haandvaerker haandvaerker)
        {
            if (ModelState.IsValid)
            {
                var data = new StringContent(JsonSerializer.Serialize(haandvaerker), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("api/haandvaerkers", data);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(haandvaerker);
        }

        // GET: Haandvaerkers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _client.GetAsync("api/haandvaerkers/" + id);
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var haandvaerker = await JsonSerializer.DeserializeAsync<Haandvaerker>(responseStream);

            //var haandvaerker = await _context.Haandvaerker.FindAsync(id);

            if (haandvaerker == null)
            {
                return NotFound();
            }
            return View(haandvaerker);
        }

        // POST: Haandvaerkers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HaandvaerkerId,HVAnsaettelsedato,HVEfternavn,HVFagomraade,HVFornavn")] Haandvaerker haandvaerker)
        {
            if (id != haandvaerker.HaandvaerkerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = new StringContent(JsonSerializer.Serialize(haandvaerker), Encoding.UTF8, "application/json");
                var response = await _client.PutAsync("api/haandvaerkers/" + id, data);
                using var responseStream = await response.Content.ReadAsStreamAsync();
                haandvaerker = await JsonSerializer.DeserializeAsync<Haandvaerker>(responseStream);
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            return View(haandvaerker);
        }

        // GET: Haandvaerkers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var response = await _client.GetAsync("api/haandvaerker/" + id);
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var haandvaerker = await JsonSerializer.DeserializeAsync<Haandvaerker>(responseStream);

            if (haandvaerker == null)
            {
                return NotFound();
            }

            return View(haandvaerker);
        }

        // POST: Haandvaerkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync("api/haandvaerkers/" + id);
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(Index));
        }
    }
}
