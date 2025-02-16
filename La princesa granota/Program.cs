using Heirloom;
using Heirloom.Desktop;
using System.Collections.Generic;

namespace movimientoPNG
{
    class Program
    {

        private const int Amplada = 2560;
        private const int Altura = 1440;
        private const int manzanaInicial = 4;

        private static Window _finestra = null!;
        private static Jesucristo _yisus = null!;
        private static List<Manzana> _manzanas = new();
        private static Image _image = null!;

        static bool crearManzanas;
        static bool esAtrapat;
        static bool atrapabaAntesManzana;
        static bool haGanado;

        private static float _tiempoVictoria = 0f;
        private const float _tiempoEspera = 2f; 
        
        static void Main(string[] args)
        {
            crearManzanas = false;
            esAtrapat = false;
            atrapabaAntesManzana = false;
            haGanado = false;
            Application.Run(() =>
            {
                _finestra = new Window("La Manzana y la Princesa", (Amplada, Altura));
                _finestra.BeginFullscreen();
                _yisus = new Jesucristo(10, 10);
                bool eva = true;
                for (int i = 0; i < manzanaInicial; i++)
                {
                    _manzanas.Add(new Manzana(eva));
                    eva = false;
                }

                _image = new Image("PNG/win.png");

                var loop = GameLoop.Create(_finestra.Graphics, OnUpdate);
                loop.Start();
            });
        }

        private static void OnUpdate(GraphicsContext gfx, float dt)
        {
            var rectanglefinestra = new Rectangle(0, 0, _finestra.Width, _finestra.Height);
            gfx.Clear(Color.Yellow);

            if (!haGanado)
            {
                _yisus.Mou(_finestra.Bounds);
            }

            bool tocaManzana = false;
            bool princesaEncontrada = false;

            foreach (var manzana in _manzanas)
            {
                if (_yisus.Atrapa(manzana))
                {
                    tocaManzana = true;

                    if (manzana._esEva)
                    {
                        manzana.cambioImatge();
                        princesaEncontrada = true;
                    }

                    if (!atrapabaAntesManzana)
                    {
                        atrapabaAntesManzana = true;
                        crearManzanas = true;
                    }
                }
            }

            if (!tocaManzana) atrapabaAntesManzana = false;

            if (crearManzanas)
            {
                for (int i = 0; i < 4; i++) _manzanas.Add(new Manzana(false));
                crearManzanas = false;
            }

            if (princesaEncontrada && !haGanado)
            {
                _tiempoVictoria = 0f;
                haGanado = true;
            }

            if (haGanado)
            {
                _tiempoVictoria += dt;

                if (_tiempoVictoria >= _tiempoEspera)
                {
                    _manzanas.Clear(); 
                    _yisus = null;
                }
            }

            _yisus?.Pinta(gfx);

            foreach (var manzana in _manzanas)
            {
                manzana.Pinta(gfx);
            }

            if (haGanado && _tiempoVictoria >= _tiempoEspera)
            {
                float winX = (_finestra.Width - _image.Width) / 2;
                float winY = (_finestra.Height - _image.Height) / 2;
                gfx.DrawImage(_image, (winX, winY));
            }
        }
    }
}