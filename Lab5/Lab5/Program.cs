using System;

public interface IRenderer
{
    void RenderSimplePage(string title, string content);
    void RenderProductPage(Product product);
}
public class HTMLRenderer : IRenderer
{
    public void RenderSimplePage(string title, string content)
    {
        Console.WriteLine($"This is a simple HTML page\n\ttitle: {title}\n\tcontent: {content}");
    }
    public void RenderProductPage(Product product)
    {
        Console.WriteLine($"This is a PRODUCT HTML page:\n\tProduct:\n\t\tid: {product.id}\n\t\tname: {product.name}\n\t\tdesc: {product.description}\n\t\timage: {product.image}");
    }
}

public class JSONRenderer : IRenderer
{
    public void RenderSimplePage(string title, string content)
    {
        Console.WriteLine($"This is a simple JSON page\n\ttitle: {title}\n\tcontent: {content}");
    }
    public void RenderProductPage(Product product)
    {
        Console.WriteLine($"This is a PRODUCT JSON page:\n\tProduct:\n\t\tid: {product.id}\n\t\tname: {product.name}\n\t\tdesc: {product.description}\n\t\timage: {product.image}");
    }
}

public class XMLRenderer : IRenderer
{
    public void RenderSimplePage(string title, string content)
    {
        Console.WriteLine($"This is a simple XML page\n\ttitle: {title}\n\tcontent: {content}");
    }
    public void RenderProductPage(Product product)
    {
        Console.WriteLine($"This is a PRODUCT XML page:\n\tProduct:\n\t\tid: {product.id}\n\t\tname: {product.name}\n\t\tdesc: {product.description}\n\t\timage: {product.image}");
    }
}
public abstract class Page
{
    protected IRenderer renderer;

    public Page(IRenderer renderer)
    {
        this.renderer = renderer;
    }

    public abstract void View();
}

public class SimplePage : Page
{
    private string title;
    private string content;
    public SimplePage(IRenderer renderer, string title, string content) : base(renderer)
    {
        this.title = title;
        this.content = content;
    }
    public override void View()
    {
        renderer.RenderSimplePage(title, content);
    }
}

public class ProductPage : Page
{
    private Product product;
    public ProductPage(IRenderer renderer, Product product) : base(renderer)
    {
        this.product = product;
    }
    public override void View()
    {
        renderer.RenderProductPage(product);
    }
}
public class Product
{
    public string name;
    public string description;
    public string image;
    public int id;
    public Product(string name, string description, string image, int id)
    {
        this.name = name;
        this.description = description;
        this.image = image;
        this.id = id;
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        IRenderer htmlRenderer = new HTMLRenderer();
        IRenderer jsonRenderer = new JSONRenderer();
        IRenderer xmlRenderer = new XMLRenderer();

        Page simplePage = new SimplePage(htmlRenderer, "html simple page", "this is the content of html page");
        simplePage.View();

        simplePage = new SimplePage(jsonRenderer, "json simple page", "this is the content of json page");
        simplePage.View();

        simplePage = new SimplePage(xmlRenderer, "xml simple page", "this is the content of xml page");
        simplePage.View();

        Product product = new Product("Soap", "It is clean", "soap.jpg", 1);
        Page productPage = new ProductPage(htmlRenderer, product);
        productPage.View();

        productPage = new ProductPage(jsonRenderer, product);
        productPage.View();

        productPage = new ProductPage(xmlRenderer, product);
        productPage.View();
    }
}
