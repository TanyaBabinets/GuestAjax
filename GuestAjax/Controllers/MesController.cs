using Azure;
using GuestAjax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace GuestAjax.Controllers
	
{
    public class MesController(MesContext context) : Controller
    {
        private readonly MesContext _context = context;



        [HttpGet]
        public async Task<IActionResult> GetAllMes()
        {
            var list = await _context.messages.Include(u => u.user).ToListAsync();
            
            if (list.Count == 0)
               return Problem("Список пустой!");
            list.ForEach(m => { m.date = m.Datetime.ToString(); });
            string response = JsonConvert.SerializeObject(list);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {


            var mes = await _context.messages.Include(m => m.user).FirstOrDefaultAsync(m => m.Id == id);
            if (mes == null)
            {
                return NotFound();
            }
            string response = JsonConvert.SerializeObject(mes);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddMes(Messages mes)
		{
			mes.user = await _context.users.FindAsync(mes.UserId);
			if (ModelState.IsValid)
            {
                _context.Add(mes);
                await _context.SaveChangesAsync();
                string response = "Отзыв успешно добавлен!";
                return Json(response);
            }
            return Problem("Не получается добавить отзыв!");
        }

        
      
        [HttpPut]
		public async Task<IActionResult> UpdateMes(Messages mes, int? id)
		{
            mes.user= await _context.users.FindAsync(mes.UserId);

			if (ModelState.IsValid)
            {
          
                var existingMes = await _context.messages.FirstOrDefaultAsync(m => m.Id == id);
                if (existingMes != null)
                {
                    
                    existingMes.message = mes.message;
                    existingMes.mark = mes.mark;
                    existingMes.Datetime = mes.Datetime;
                   existingMes.user = mes.user;
                    await _context.SaveChangesAsync();
                    string response = "Отзыв успешно изменен!";
                    return Json(response);
                }
            
                 else
                {
                    return Problem("Сообщение не найдено.");
                }
            }
            return Problem("Проблемы при редактировании отзыва!");
        }
        [HttpDelete]
		public async Task<IActionResult> DeleteMes(int id)
		{
			if (_context.messages == null)
			{
				return Problem("Список  пустой!");
			}
			var m = await _context.messages.FindAsync(id);
			if (m != null)
			{
				_context.messages.Remove(m);
			}
			await _context.SaveChangesAsync();
			string response = "Отзыв успешно удален!";
			return Json(response);
		}
		
	}
}
