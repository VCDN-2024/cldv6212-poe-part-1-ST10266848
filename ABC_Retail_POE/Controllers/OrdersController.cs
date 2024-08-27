using ABC_Retail_POE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABC_Retail_POE.Controllers
{
    public class OrdersController : Controller
    {
        private readonly TableStorageService _tableStorageService;
        private readonly QueueService _queueService;

        public OrdersController(TableStorageService tableStorageService, QueueService queueService)
        {
            _tableStorageService = tableStorageService;
            _queueService = queueService;
        }

        // Action to display all orders
        public async Task<IActionResult> Index()
        {
            var orders = await _tableStorageService.GetAllOrdersAsync();
            return View(orders);
        }
        public async Task<IActionResult> Create()
        {
            var users = await _tableStorageService.GetAllUsersAsync();
            var products = await _tableStorageService.GetAllProductsAsync();

            // Check for null or empty lists
            if (users == null || users.Count == 0)
            {
                // Handle the case where no users are found
                ModelState.AddModelError("", "No users found! Please add users first!");
                return View(); // Or redirect to another action
            }

            if (products == null || products.Count == 0)
            {
                // Handle the case where no birds are found
                ModelState.AddModelError("", "No products found! Please add products first!");
                return View(); // Or redirect to another action
            }

            ViewData["Users"] = users;
            ViewData["Products"] = products;

            return View();
        }

        // Action to handle the form submission and create the order
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (!ModelState.IsValid)
            {//TableService
                order.OrderDate = DateTime.SpecifyKind(order.OrderDate, DateTimeKind.Utc);
                order.PartitionKey = "OrdersPartition";
                order.RowKey = Guid.NewGuid().ToString();
                order.OrderId = order.RowKey;

                await _tableStorageService.AddOrderAsync(order);
                //MessageQueue
                string notification = $"New order by User {order.UserId} for Product {order.ProductId} on {order.OrderDate}";
                await _queueService.SendMessageAsync(notification);

                return RedirectToAction("Index");
            }
            else
            {
                // Log model state errors
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
            }

            // Reload users and products lists if validation fails
            var users = await _tableStorageService.GetAllUsersAsync();
            var products = await _tableStorageService.GetAllProductsAsync();
            ViewData["Users"] = users;
            ViewData["Products"] = products;

            return View(order);
        }

        public async Task<IActionResult> Delete(string partitionKey, string rowKey)
        {
            await _tableStorageService.DeleteOrderAsync(partitionKey, rowKey);
            return RedirectToAction("Index");
        }


    }
}
