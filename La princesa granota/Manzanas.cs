using Heirloom;

namespace movimientoPNG;

public class Manzana
{
    Random random = new Random();
    private Vector _posicio;
    private Image _image;
    private Rectangle _rectangle;
    public bool _esEva { get; set; }

    public Manzana(bool esEva) 
    {
        _image = new Image("PNG/manzana.png"); 
        _posicio = new Vector(random.NextFloat(0, 2560), random.NextFloat(0, 1440));
        _esEva = esEva;
        _rectangle = new Rectangle(_posicio.X, _posicio.Y, _image.Width, _image.Height);
    }

    public Rectangle Posicio()
    {
        return new Rectangle(_posicio, _image.Size);
    }

    public void cambioImatge()
    {
        if (_esEva) 
        {
            _image = new Image("PNG/princesa.png"); 
        }
    }

    public void Pinta(GraphicsContext gfx)
    {
        gfx.DrawImage(_image, _posicio);
    }
}