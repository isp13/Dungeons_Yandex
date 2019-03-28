using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System;

public class MapGenerator : MonoBehaviour
{
    
    public Tilemap tilemap;
    public Tile floor;
    
    Program method = new Program();
    public int current_room_x=0;
    public int current_room_y=0;
    private void LateUpdate()
    {

       
    }

    void Start()
    {
        
        method.create_suitable_labyrinth(20, 25);
        current_room_x = method.get_length()/2;
        current_room_y = method.get_length()/2;
        
        form_current_room();
        
    }

    public void form_current_room()
    {
        Program.room_point room = method.rooms[current_room_x,current_room_y];
        int length = Math.Abs(room.rx2 - room.rx1);
        int height = Math.Abs(room.ry2 - room.ry1);
        Debug.Log("length");
        Debug.Log(length);
        Debug.Log("height");
        Debug.Log(height);


        for (int y = 0; y < height/10; ++y)
        {
            Debug.Log(y);
            for (int x = 0; x < length/10; ++x)
            {
                //tilemap.SetTile(new Vector3Int(y, x, 0), floor);
                
            }
        }

        System.Random rand = new System.Random();
        int rnd = rand.Next(1,100);
        // 1-35  - rectangle room
        // 36-55 - rectangle room with column in the middle
        // 56-75 - corner room (triangle)
        // 76-99 - random shit

    }
    void func(int[,] map)
    {
        
        BoundsInt bounds = tilemap.cellBounds; // getting all allowed x and y in tilemap (getting its size)
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds); // getting info of tiles from tilemap
        Vector3Int currentCell = tilemap.WorldToCell(transform.position); //current x,y,z position 

        for (int x = 0; x < map.GetLength(0); ++x)
        {
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), floor);
            }
        }
    }
}

class Program
{
    public class room_point
    {
        public int x, y;
        public int rx1, ry1, rx2, ry2;
        public int up, right, down, left;// -1 for unknown, 0 for none, 1 - need a passage
        public int drawn;
 
        public room_point()
        {
            this.x = 0;
            this.y = 0;
            this.rx1 = 0;
            this.ry1 = 0;
            this.rx2 = 0;
            this.ry2 = 0;
            this.up = -1;
            this.right = -1;
            this.down = -1;
            this.left = -1;
            this.drawn = 0;
        }
        public room_point(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.rx1 = 0;
            this.ry1 = 0;
            this.rx2 = 0;
            this.ry2 = 0;
            this.up = -1;
            this.right = -1;
            this.down = -1;
            this.left = -1;
            this.drawn = 0;
        }
 
    }
 
    const int labirynth_arr_size = 50;
 
    char[,] print_array = new char[labirynth_arr_size * 2, labirynth_arr_size * 2];
 
 
    public room_point[,] rooms = new room_point[1000, 1000];
 
    //FUNCTOINS DECLARATION
 
    //pass
 
    //PARAMETERS
    public int max_room_size = 400;
    public int movex = 0;
    public int movey = 0;
    public double draw_size = 1;
    //--------------
 
    //tmp-variables (don't change!!!)
    public int max_depth = 0;
    public int max_depth_i = labirynth_arr_size / 2;
    public int max_depth_j = labirynth_arr_size / 2;
    //------------
    //FUNCTIONS IMPLIMENTATION
    public System.Random rand1 = new System.Random();
    
    public int get_length()
    {
        return labirynth_arr_size;
    }
    public int super_rand()
    {
        int out_of_a = 100;
        int out_of_b = 35;
        if (rand1.Next(1, 100) < out_of_b)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
 
    public int room_size_random()
    {
        int outt = max_room_size / 8;
        outt += rand1.Next(1, 100) % (max_room_size / 2 - outt);
        return outt;
 
    }
 
    public void create_labyrinth(int i, int j)
    {
        
        rooms[i, j].drawn = 1;
        rooms[i, j].rx1 = -room_size_random();
        rooms[i, j].ry1 = -room_size_random();
        rooms[i, j].rx2 = room_size_random();
        rooms[i, j].ry2 = room_size_random();
 
        if (rooms[i, j].up == 1 && rooms[i, j - 1].drawn == 0)
        {
            if (rooms[i, j - 2].down == -1)
            {
                rooms[i, j - 1].up = super_rand();
            }
            else if (rooms[i, j - 2].down == 0)
            {
                rooms[i, j - 1].up = 0;
            }
            else //need a passage
            {
                rooms[i, j - 1].up = 1;
            }
            //--------------------------------
            if (rooms[i + 1, j - 1].left == -1)
            {
                rooms[i, j - 1].right = super_rand();
            }
            else if (rooms[i + 1, j - 1].left == 0)
            {
                rooms[i, j - 1].right = 0;
            }
            else //need a passage
            {
                rooms[i, j - 1].right = 1;
            }
            //--------------------------------
            rooms[i, j - 1].down = 1;
            //--------------------------------
            if (rooms[i - 1, j - 1].right == -1)
            {
                rooms[i, j - 1].left = super_rand();
            }
            else if (rooms[i - 1, j - 1].right == 0)
            {
                rooms[i, j - 1].left = 0;
            }
            else //need a passage
            {
                rooms[i, j - 1].left = 1;
            }
 
            rooms[i, j - 1].x = rooms[i, j].x;
            rooms[i, j - 1].y = rooms[i, j].y - max_room_size;
            create_labyrinth(i, j - 1);
        }
        if (rooms[i, j].right == 1 && rooms[i + 1, j].drawn == 0)
        {
            if (rooms[i + 1, j - 1].down == -1)
            {
                rooms[i + 1, j].up = super_rand();
            }
            else if (rooms[i + 1, j - 1].down == 0)
            {
                rooms[i + 1, j].up = 0;
            }
            else //need a passage
            {
                rooms[i + 1, j].up = 1;
            }
            //--------------------------------
            if (rooms[i + 2, j].left == -1)
            {
                rooms[i + 1, j].right = super_rand();
            }
            else if (rooms[i + 2, j].left == 0)
            {
                rooms[i + 1, j].right = 0;
            }
            else //need a passage
            {
                rooms[i + 1, j].right = 1;
            }
            //--------------------------------
            if (rooms[i + 1, j + 1].up == -1)
            {
                rooms[i + 1, j].down = super_rand();
            }
            else if (rooms[i + 1, j + 1].up == 0)
            {
                rooms[i + 1, j].down = 0;
            }
            else //need a passage
            {
                rooms[i + 1, j].down = 1;
            }
            //--------------------------------
            rooms[i + 1, j].left = 1;
 
            rooms[i + 1, j].x = rooms[i, j].x + max_room_size;
            rooms[i + 1, j].y = rooms[i, j].y;
            create_labyrinth(i + 1, j);
        }
        if (rooms[i, j].down == 1 && rooms[i, j + 1].drawn == 0)
        {
            rooms[i, j + 1].up = 1;
            //--------------------------------
            if (rooms[i + 1, j + 1].left == -1)
            {
                rooms[i, j + 1].right = super_rand();
            }
            else if (rooms[i + 1, j + 1].left == 0)
            {
                rooms[i, j + 1].right = 0;
            }
            else //need a passage
            {
                rooms[i, j + 1].right = 1;
            }
            //--------------------------------
            if (rooms[i, j + 2].up == -1)
            {
                rooms[i, j + 1].down = super_rand();
            }
            else if (rooms[i, j + 2].up == 0)
            {
                rooms[i, j + 1].down = 0;
            }
            else //need a passage
            {
                rooms[i, j + 1].down = 1;
            }
            //--------------------------------
            if (rooms[i - 1, j + 1].right == -1)
            {
                rooms[i, j + 1].left = super_rand();
            }
            else if (rooms[i - 1, j + 1].right == 0)
            {
                rooms[i, j + 1].left = 0;
            }
            else //need a passage
            {
                rooms[i, j + 1].left = 1;
            }
 
 
            rooms[i, j + 1].x = rooms[i, j].x;
            rooms[i, j + 1].y = rooms[i, j].y + max_room_size;
            create_labyrinth(i, j + 1);
        }
        if (rooms[i, j].left == 1 && rooms[i - 1, j].drawn == 0)
        {
            if (rooms[i - 1, j - 1].down == -1)
            {
                rooms[i - 1, j].up = super_rand();
            }
            else if (rooms[i - 1, j - 1].down == 0)
            {
                rooms[i - 1, j].up = 0;
            }
            else //need a passage
            {
                rooms[i - 1, j].up = 1;
            }
            //--------------------------------
            rooms[i - 1, j].right = 1;
            //--------------------------------
            if (rooms[i - 1, j + 1].up == -1)
            {
                rooms[i - 1, j].down = super_rand();
            }
            else if (rooms[i - 1, j + 1].up == 0)
            {
                rooms[i - 1, j].down = 0;
            }
            else //need a passage
            {
                rooms[i - 1, j].down = 1;
            }
            //--------------------------------
            if (rooms[i - 2, j].right == -1)
            {
                rooms[i - 1, j].left = super_rand();
            }
            else if (rooms[i - 2, j].right == 0)
            {
                rooms[i - 1, j].left = 0;
            }
            else //need a passage
            {
                rooms[i - 1, j].left = 1;
            }
 
            rooms[i - 1, j].x = rooms[i, j].x - max_room_size;
            rooms[i - 1, j].y = rooms[i, j].y;
            create_labyrinth(i - 1, j);
        }
 
    }
 
    public void create_suitable_labyrinth(int min_rooms, int max_rooms)
    {
        Debug.Log("Creating labyrinth...");
        int not_ok = 1;
        while (Convert.ToBoolean(not_ok))
        {
            //Getting ready (erasing previous)
            for (int a = 0; a < labirynth_arr_size; a++)
            {
                for (int b = 0; b < labirynth_arr_size; b++)
                {
                    rooms[a, b] = new room_point(0, 0);
                }
            }
            rooms[labirynth_arr_size / 2, labirynth_arr_size / 2].up = 1;
            rooms[labirynth_arr_size / 2, labirynth_arr_size / 2].right = 1;
            rooms[labirynth_arr_size / 2, labirynth_arr_size / 2].down = 1;
            rooms[labirynth_arr_size / 2, labirynth_arr_size / 2].left = 1;
            rooms[labirynth_arr_size / 2, labirynth_arr_size / 2].x = 0;
            rooms[labirynth_arr_size / 2, labirynth_arr_size / 2].y = 0;
            //Creating
            create_labyrinth(labirynth_arr_size / 2, labirynth_arr_size / 2);
            //Cleaning up & cheking validness
            int count = 0;
            for (int a = 0; a < labirynth_arr_size; a++)
            {
                for (int b = 0; b < labirynth_arr_size; b++)
                {
                    if (Convert.ToBoolean(rooms[a, b].drawn))
                    {
                        count++;
                    }
                    rooms[a, b].drawn = 0;
                }
            }
            if (count >= min_rooms && count <= max_rooms)
            {
                not_ok = 0;
            }
        }
 
        //creating finish room
        max_depth = 0;
        find_deepest(labirynth_arr_size / 2, labirynth_arr_size / 2, 0);
    }
 
    public void find_deepest(int i, int j, int depth)
    {
        rooms[i, j].drawn = 1;
        if (depth >= max_depth)
        {
            max_depth = depth;
            max_depth_i = i;
            max_depth_j = j;
        }
        depth++;
        if (rooms[i, j].up == 1 && rooms[i, j - 1].drawn == 0)
        {
            find_deepest(i, j - 1, depth);
        }
        if (rooms[i, j].right == 1 && rooms[i + 1, j].drawn == 0)
        {
            find_deepest(i + 1, j, depth);
        }
        if (rooms[i, j].down == 1 && rooms[i, j + 1].drawn == 0)
        {
            find_deepest(i, j + 1, depth);
        }
        if (rooms[i, j].left == 1 && rooms[i - 1, j].drawn == 0)
        {
            find_deepest(i - 1, j, depth);
        }
    }
 
    public void print()
    {
        //empty array
        for (int i = 0; i < labirynth_arr_size * 2; i++)
        {
            for (int j = 0; j < labirynth_arr_size * 2; j++)
            {
                print_array[i, j] = ' ';
            }
        }
        //clean up
        for (int a = 0; a < labirynth_arr_size; a++)
        {
            for (int b = 0; b < labirynth_arr_size; b++)
            {
                rooms[a, b].drawn = 0;
            }
        }
 
        //fill array
        go_through(labirynth_arr_size / 2, labirynth_arr_size / 2);
 
        //print array
        for (int i = 0; i < labirynth_arr_size * 2; i++)
        {
            for (int j = 0; j < labirynth_arr_size * 2; j++)
            {
                Debug.Log(print_array[j, i]);
            }
            Debug.Log('\n');
        }
 
        //clean up
        for (int a = 0; a < labirynth_arr_size; a++)
        {
            for (int b = 0; b < labirynth_arr_size; b++)
            {
                rooms[a, b].drawn = 0;
            }
        }
    }
 
    public void go_through(int i, int j)
    {
        rooms[i, j].drawn = 1;
        print_array[i * 2, j * 2] = '#';
 
        if (rooms[i, j].up == 1 && rooms[i, j - 1].drawn == 0)
        {
            go_through(i, j - 1);
        }
        if (rooms[i, j].right == 1 && rooms[i + 1, j].drawn == 0)
        {
            go_through(i + 1, j);
        }
        if (rooms[i, j].down == 1 && rooms[i, j + 1].drawn == 0)
        {
            go_through(i, j + 1);
        }
        if (rooms[i, j].left == 1 && rooms[i - 1, j].drawn == 0)
        {
            go_through(i - 1, j);
        }
 
        //Draw_tunnels
 
        if (rooms[i, j].up == 1)
        {
            print_array[i * 2, j * 2 - 1] = '|';
        }
        if (rooms[i, j].right == 1)
        {
            print_array[i * 2 + 1, j * 2] = '-';
        }
        if (rooms[i, j].down == 1)
        {
            print_array[i * 2, j * 2 + 1] = '|';
        }
        if (rooms[i, j].left == 1)
        {
            print_array[i * 2 - 1, j * 2] = '-';
        }
    }
    
    public room_point print_room(int i, int j)
    {
        return rooms[i,j];
    }
 
    static void Main()
    {
        
        
        
    }
}
