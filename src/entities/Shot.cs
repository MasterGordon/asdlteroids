class Shot : Renderable, Logic
{
    const double SPEED = 300 * Scene.SCALE;
    private double dx, dy;
    public double X, Y;

    public Shot(double x, double y, double rotation)
    {
        this.X = x;
        this.Y = y;
        dx = Math.Cos(rotation) * SPEED;
        dy = Math.Sin(rotation) * SPEED;
    }

    public void Render(Renderer renderer, double dx)
    {
        renderer.setColor(255, 255, 255);
        renderer.DrawRect(X, Y, 2, 2);
    }

    public void Update(KeyState keyState, double dx)
    {
        X += dx * this.dx;
        Y += dx * this.dy;
        if (X < 0 || X > Scene.SCREEN_WIDTH || Y < 0 || Y > Scene.SCREEN_HEIGHT)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Scene.Instance.Shots.Remove(this);
    }
}
