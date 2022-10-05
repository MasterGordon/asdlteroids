enum Size : int
{
    Large = 60,
    Medium = 30,
    Small = 20
}

class Asteroid : Renderable, Logic
{
    const double SPEED = 100 * Scene.SCALE;
    private double dx, dy;
    public double X, Y;
    public Size Size;
    private double[][] shape;

    public Asteroid(double x, double y, double rotation, Size size)
    {
        this.X = x;
        this.Y = y;
        this.Size = size;
        dx = Math.Cos(rotation) * SPEED;
        dy = Math.Sin(rotation) * SPEED;
        var random = new Random();
        shape = new double[16][];
        for (int i = 0; i < shape.Length; i++)
        {
            var rad = i * Math.PI * 2 / shape.Length;
            shape[i] = new double[2];
            shape[i][0] = Math.Cos(rad) * (0.5 + random.NextDouble() / 2) * (int)size / 2 * Scene.SCALE;
            shape[i][1] = Math.Sin(rad) * (0.5 + random.NextDouble() / 2) * (int)size / 2 * Scene.SCALE;
        }
    }

    public void Render(Renderer renderer, double dx)
    {
        renderer.setColor(255, 255, 255);
        double[][] drawLines = new double[shape.Length + 1][];
        var halfSize = (int)Size / 2 * Scene.SCALE;
        for (int i = 0; i < shape.Length; i++)
        {
            drawLines[i] = new double[2];
            drawLines[i][0] = shape[i][0] + X - halfSize;
            drawLines[i][1] = shape[i][1] + Y - halfSize;
        }
        drawLines[drawLines.Length - 1] = new double[2];
        drawLines[drawLines.Length - 1][0] = shape[0][0] + X - halfSize;
        drawLines[drawLines.Length - 1][1] = shape[0][1] + Y - halfSize;
        renderer.DrawLines(drawLines);
    }

    public void Update(KeyState keyState, double dx)
    {
        X += dx * this.dx;
        Y += dx * this.dy;
        this.X = this.X % Scene.SCREEN_WIDTH;
        this.Y = this.Y % Scene.SCREEN_HEIGHT;
        if (this.X < 0)
        {
            this.X += Scene.SCREEN_WIDTH;
        }
        if (this.Y < 0)
        {
            this.Y += Scene.SCREEN_HEIGHT;
        }

        foreach (var shot in Scene.Instance.Shots)
        {
            var halfSize = (int)Size / 2 * Scene.SCALE;
            if (Point.Distance(shot.X, shot.Y, X - halfSize, Y - halfSize) < halfSize)
            {
                shot.Destroy();
                this.Destroy();
                var rotation = Math.Atan2(Y - shot.Y, X - shot.X);
                if (Size == Size.Large)
                {
                    Scene.Instance.Asteroids.Add(new Asteroid(X, Y, rotation + Math.PI * 2, Size.Medium));
                    Scene.Instance.Asteroids.Add(new Asteroid(X, Y, rotation + Math.PI * 2 * 0.3, Size.Medium));
                    Scene.Instance.Asteroids.Add(new Asteroid(X, Y, rotation + Math.PI * 2 * 0.6, Size.Medium));
                }
                if (Size == Size.Medium)
                {
                    Scene.Instance.Asteroids.Add(new Asteroid(X, Y, rotation + Math.PI * 2 * 0.3, Size.Small));
                    Scene.Instance.Asteroids.Add(new Asteroid(X, Y, rotation + Math.PI * 2 * 0.6, Size.Small));
                }
                break;
            }
        }
    }

    public void Destroy()
    {
        Scene.Instance.AudioPlayer.Play(Sound.EXPLOSION);
        Scene.Instance.Score += (int)Size;
        Scene.Instance.Asteroids.Remove(this);
    }
}
