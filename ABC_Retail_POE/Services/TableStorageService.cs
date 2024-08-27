using Azure;
using Azure.Data.Tables;
using ABC_Retail_POE.Models;
using System.Threading.Tasks;

public class TableStorageService
{
    private readonly TableClient _tableClient;
    private readonly TableClient _userTableClient;
    private readonly TableClient _orderTableClient;

    public TableStorageService(string connectionString)
    {
        _tableClient = new TableClient(connectionString, "Products");
        _userTableClient = new TableClient(connectionString, "Users");
        _orderTableClient = new TableClient(connectionString, "Orders");
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = new List<Product>();

        await foreach (var product in _tableClient.QueryAsync<Product>())
        {
            products.Add(product);
        }

        return products;
    }

    public async Task AddProductAsync(Product product)
    {
        // Ensure PartitionKey and RowKey are set
        if (string.IsNullOrEmpty(product.PartitionKey) || string.IsNullOrEmpty(product.RowKey))
        {
            throw new ArgumentException("PartitionKey and RowKey must be set.");
        }

        try
        {
            await _tableClient.AddEntityAsync(product);
        }
        catch (RequestFailedException ex)
        {
            // Handle exception as necessary, for example log it or rethrow
            throw new InvalidOperationException("Error adding entity to Table Storage", ex);
        }
    }

    public async Task DeleteProductAsync(string partitionKey, string rowKey)
    {
        await _tableClient.DeleteEntityAsync(partitionKey, rowKey);
    }

    public async Task<Product?> GetProductAsync(string partitionKey, string rowKey)
    {
        try
        {
            var response = await _tableClient.GetEntityAsync<Product>(partitionKey, rowKey);
            return response.Value;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            // Handle not found
            return null;
        }
    }



    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = new List<User>();

        await foreach (var user in _userTableClient.QueryAsync<User>())
        {
            users.Add(user);
        }

        return users;
    }
    public async Task AddUserAsync(User user)
    {
 
        if (string.IsNullOrEmpty(user.PartitionKey) || string.IsNullOrEmpty(user.RowKey))
        {
            throw new ArgumentException("PartitionKey and RowKey must be set.");
        }

        try
        {
            await _userTableClient.AddEntityAsync(user);
        }
        catch (RequestFailedException ex)
        {
            throw new InvalidOperationException("Error adding entity to Table Storage", ex);
        }

    }

    public async Task DeleteUserAsync(string partitionKey, string rowKey)
    {
        await _userTableClient.DeleteEntityAsync(partitionKey, rowKey);
    }

    public async Task<User?> GetUserAsync(string partitionKey, string rowKey)
    {
        try
        {
            var response = await _userTableClient.GetEntityAsync<User>(partitionKey, rowKey);
            return response.Value;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    public async Task AddOrderAsync(Order order)
    {
        if (string.IsNullOrEmpty(order.PartitionKey) || string.IsNullOrEmpty(order.RowKey))
        {
            throw new ArgumentException("PartitionKey and RowKey must be set.");
        }

        try
        {
            await _orderTableClient.AddEntityAsync(order);
        }
        catch (RequestFailedException ex)
        {
            throw new InvalidOperationException("Error adding sighting to Table Storage", ex);
        }
    }

    public async Task DeleteOrderAsync(string partitionKey, string rowKey)
    {
        await _orderTableClient.DeleteEntityAsync(partitionKey, rowKey);
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var orders = new List<Order>();

        await foreach (var order in _orderTableClient.QueryAsync<Order>())
        {
            orders.Add(order);
        }

        return orders;
    }

}


