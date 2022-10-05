class Ufo : Logic, Renderable
{
    public int X, Y;

    public Ufo(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Render(Renderer renderer, double dx)
    {
        renderer.setColor(255, 255, 255);
        renderer.DrawLines(new double[][]{
            new double[]{X-Scene.SCALE*15, Y},
            new double[]{X+Scene.SCALE*15, Y},
            });
        renderer.DrawLines(new double[][]{
            new double[]{X-Scene.SCALE*15, Y},
            new double[]{X-Scene.SCALE*6, Y+Scene.SCALE*6},
            new double[]{X+Scene.SCALE*6, Y+Scene.SCALE*6},
            new double[]{X+Scene.SCALE*15, Y},
            });
        renderer.DrawLines(new double[][]{
            new double[]{X-Scene.SCALE*15, Y},
            new double[]{X-Scene.SCALE*6, Y-Scene.SCALE*6},
            new double[]{X+Scene.SCALE*6, Y-Scene.SCALE*6},
            new double[]{X+Scene.SCALE*15, Y},
            });
        renderer.DrawLines(new double[][]{
            new double[]{X-Scene.SCALE*6, Y-Scene.SCALE*6},
            new double[]{X-Scene.SCALE*4, Y-Scene.SCALE*12},
            new double[]{X+Scene.SCALE*4, Y-Scene.SCALE*12},
            new double[]{X+Scene.SCALE*6, Y-Scene.SCALE*6},
            });
    }

    public void Update(KeyState keyState, double dx)
    {
        //
    }
}
