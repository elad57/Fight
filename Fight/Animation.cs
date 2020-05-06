using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fight
{
    class animation
    {
        public Texture2D texture { get; set; }
       
        public int frames { get; set; }
        public List<Rectangle> rec { get; private set; }
        public List<Vector2> org { get; private set; }
        public List<Rectangle> hitbox { get; private set; }
        public List<Vector2> knockback { get; private set; }
        public List<Circle> head { get; private set; }
        public List<Circle> body { get; private set; }
        public List<Circle> legs { get; private set; }
        public float speed { get; set; }
        public int index { get; set; }

        public bool isloopable { get; set; }
        public animation(Folders f,status s,ContentManager cm)
        {
            string name = "fighters/"+f.ToString() + "/" + s.ToString();

            texture = cm.Load<Texture2D>(name);
            speed = 0.1f;
            head = new List<Circle>();
            body = new List<Circle>();
            legs = new List<Circle>();
            hitbox = new List<Rectangle>();
            knockback = new List<Vector2>();
            isloopable = false;
            Processing1(cm, name);
            maketranspernt();
        }
        private void maketranspernt()
        {
            Color[] data = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(data);
            Color trans = data[0];
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == trans)
                    data[i] = Color.Transparent;
            }
            texture.SetData<Color>(data);
        }
       private void Processing1(ContentManager cm, string name)
        {
            string maskname = name + "mask";

            Texture2D mask = cm.Load<Texture2D>(maskname);

            Color[] maskcol = new Color[mask.Height * mask.Width];
           
            mask.GetData<Color>(maskcol);
            // load mask to Color array

            rec = new List<Rectangle>();
            org = new List<Vector2>();
            List<int> pnt = new List<int>();
            //load lists

            Color[] recpoints = new Color[mask.Width];
            mask.GetData<Color>(0, new Rectangle(0, mask.Height - 1, mask.Width, 1), recpoints, 0, mask.Width);
            
            for (int i = 0; i < recpoints.Length; i++)
            {
                if (recpoints[i] != recpoints[1])
                    pnt.Add(i);
            }
            //find rectengale points

            for (int i = 1; i < pnt.Count; i += 2)
            {
                org.Add(new Vector2(pnt[i] - pnt[i - 1], (mask.Height - 1)/2));
                rec.Add(new Rectangle(pnt[i - 1], 0, pnt[i + 1] - pnt[i - 1], mask.Height - 1));

            }
            frames = rec.Count;
            //create rectengals

            List<Circle> circles=new List<Circle>();
            int index1;
            for (int row = 0; row < mask.Height; row++)
            {
                for (int col = 3; col < mask.Width; col++)
                {
                    index1 = col + row * mask.Width;
                    if (maskcol[index1] == maskcol[0])
                    {
                        int j = 0;
                        while (maskcol[index1 + j] != maskcol[1])
                            j++;
                        circles.Add(new Circle(new Vector2(col, row), j));
                    }
                    
                }
            }
            //find circles
            int length = circles.Count;
            for (int i = 0; i < length; i++)
            {
                if (i < length / 3)
                    head.Add(circles[i]);
                else
                {
                    if (i >= 2 * length / 3 )
                        legs.Add(circles[i]);
                    else
                        body.Add(circles[i]);

                }

                
            }
            Color[] points;
            List<Vector2> temp;
            for (int i = 0; i < rec.Count; i++)
            {
                points = new Color[rec[i].Width * rec[i].Height];
                temp = new List<Vector2>();
                if(points[0]==maskcol[2])
                {
                    for (int col = 3; col <rec[i].Width; col++)
                    {
                        for (int row = 0; row < rec[i].Height; row++)
                        {
                            if(points[row*rec[i].Width+col]==maskcol[2])
                            {
                                temp.Add(new Vector2(col, row));
                            }
                        }
                    }
                    knockback.Add(temp[temp.Count - 1] - temp[0]);

                }
                else
                {
                    knockback.Add(new Vector2(0));
                }
            }
          
            /*
            for (int i = 0; i < rec.Count; i++)
            {
                
                recscan = new Color[rec[i].Width * rec[i].Height];
                mask.GetData(0, rec[i], recscan, 0, rec[i].Width * rec[i].Height);
                

                for (int col = 3; col < rec[i].Width; col++)
                {
                    for (int row = 0; row < rec[i].Height; row++)
                    {
                        index1 = col + row * mask.Width;
                        if (recscan[index1] == maskcol[2])
                        {
                            pointshit.Add(new Vector2(row, col));
                        }

                    }
                }
                if(pointshit.Count!=0)
                {
                
                    Vector2 corner = new Vector2(
                        Math.Min(pointshit[0].X, pointshit[1].X), 
                        Math.Min(pointshit[0].Y, pointshit[1].Y));
                    hitbox.Add(new Rectangle((int)corner.X, (int)corner.Y,
                        (int)Math.Abs(pointshit[0].X - pointshit[1].X), (int)Math.Abs(pointshit[0].Y - pointshit[1].Y)));

                    if(pointshit[0].X>pointshit[1].X)
                    {
                        knockback.Add(pointshit[1] - pointshit[0]);
                    }
                    else
                    {
                        knockback.Add(pointshit[0] - pointshit[1]);
                    }
                    pointshit.Clear();
                }
                else
                {
                    hitbox.Add(new Rectangle(0,0,0,0));
                    knockback.Add(new Vector2(0));
                }
            }*/

        }


    }
    class Circle
    {
        public Vector2 center;
        public float radius;
        public Circle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }
        
    }
}
