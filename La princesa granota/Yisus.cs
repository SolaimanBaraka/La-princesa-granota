using Heirloom;
using Heirloom.Desktop;

namespace movimientoPNG;

public class Jesucristo
{
    private static Image _image;
    private static Vector _posicio;
    private static int _velocidad;
    private Rectangle _rectangulo;

    public Jesucristo(int x, int y)
    {
        _image = new Image("PNG/Jesus.png");
        _velocidad = 20;
        _posicio = new Vector(x, y);
        _rectangulo = new Rectangle(_posicio, _image.Size);
    }

    public void Pinta(GraphicsContext gfx)
    {
        gfx.DrawImage(_image, _posicio);
    }

    public void Mou(Rectangle finestra)
    {
        var novaposicio = new Rectangle(_posicio, _image.Size);

        if (Input.CheckKey(Key.A, ButtonState.Down))
        {
            novaposicio.X -= _velocidad;
        }

        if (Input.CheckKey(Key.D, ButtonState.Down))
        {
            novaposicio.X += _velocidad;
        }

        if (Input.CheckKey(Key.W, ButtonState.Down)) 
        {
            novaposicio.Y -= _velocidad;
        }

        if (Input.CheckKey(Key.S, ButtonState.Down))
        {
            novaposicio.Y += _velocidad;
        }

        if (finestra.Contains(novaposicio))
        {
            _posicio = novaposicio.Position;
            _rectangulo = new Rectangle(_posicio, _image.Size);
        }
    }

    public bool Atrapa(Manzana manzana)
    {
        var rectangleManzana = manzana.Posicio();
        return _rectangulo.Overlaps(rectangleManzana);
    }
}