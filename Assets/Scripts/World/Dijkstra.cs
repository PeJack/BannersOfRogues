// using BannersOfRogues.World;
// using System.Collections.Generic;
// using RogueSharp;
// using UnityEngine;

// namespace BannersOfRogues.World {
//     public class Dijkstra {
//         private Dungeon map;
//         private int[,] graph;

//         public Dijkstra(Dungeon map, List<ICell> goals) {
//             this.map = map;
//             createGraph(goals);
//         }

//         private void createGraph(List<ICell> goals) {
//             graph = new int[map.Width, map.Height];

//             for (int x = 0; x < graph.GetLength(0); x++) {
//                 for (int y = 0; y < graph.GetLength(1); y++) {
//                     graph[x,y] = 100;
//                 }
//             }

//             for (int i = 0; i < goals.Count; i++) {
//                 ICell goal = goals[i];

//                 graph[goal.X, goal.Y] = 0;
//             }

//             bool dirty = true;

//             while (dirty) {
//                 bool changed = false;

//                 for (int x = 0; x < graph.GetLength(0); x++) {
//                     for (int y = 0; y < graph.GetLength(1); y++) {
//                         if (map.GetCell(x, y).IsWalkable) {
//                             int bestNeighbour = 100;

//                             // Север
//                             if (map.GetCell(x, y + 1).IsWalkable) {
//                                 if (graph[x, y + 1] < bestNeighbour) {
//                                     bestNeighbour = graph[x, y + 1];
//                                 }
//                             }

//                             // Северо-восток
//                             if (map.GetCell(x + 1, y + 1).IsWalkable) {
//                                 if (graph[x + 1, y + 1] < bestNeighbour) {
//                                     bestNeighbour = graph[x + 1, y + 1];
//                                 }
//                             } 

//                             // Восток
//                             if (map.GetCell(x + 1, y).IsWalkable) {
//                                 if (graph[x + 1, y] < bestNeighbour) {
//                                     bestNeighbour = graph[x + 1, y];
//                                 }
//                             }   

//                             // Юго-восток
//                             if (map.GetCell(x + 1, y - 1).IsWalkable) {
//                                 if (graph[x + 1, y - 1] < bestNeighbour) {
//                                     bestNeighbour = graph[x + 1, y - 1];
//                                 }
//                             }

//                             // Юг
//                             if (map.GetCell(x, y - 1).IsWalkable) {
//                                 if (graph[x, y - 1] < bestNeighbour) {
//                                     bestNeighbour = graph[x, y - 1];
//                                 }
//                             } 

//                             // Юго-запад
//                             if (map.GetCell(x - 1, y - 1).IsWalkable) {
//                                 if (graph[x - 1, y - 1] < bestNeighbour) {
//                                     bestNeighbour = graph[x - 1, y - 1];
//                                 }
//                             }

//                             // Запад
//                             if (map.GetCell(x - 1, y).IsWalkable) {
//                                 if (graph[x - 1, y] < bestNeighbour) {
//                                     bestNeighbour = graph[x - 1, y];
//                                 }
//                             }

//                             // Северо-запад
//                             if (map.GetCell(x - 1, y + 1).IsWalkable) {
//                                 if (graph[x - 1, y + 1] < bestNeighbour) {
//                                     bestNeighbour = graph[x - 1, y + 1];
//                                 }
//                             }  

//                             if (graph[x, y] > bestNeighbour + 2) {
//                                 graph[x, y] = bestNeighbour + 1;
//                                 changed = true;
//                             }                                                                                                                                                                                                 
//                         }
//                     }                  
//                 }
                
//                 dirty = changed;                  
//             }
//         }

//         public List<ICell> findPath(ICell start) {
//             List<ICell> path = new List<ICell>();
//             path.Add(start);

//             bool buildingPath = true;

//             while (buildingPath) {
//                 int lastX = path[path.Count - 1].X;
//                 int lastY = path[path.Count - 1].Y;
//                 int lastValue = get(lastX, lastY);

//                 if (lastValue == 0) {
//                     buildingPath = false;
//                 }
                
//                 // Север
//                 if (map.GetCell(lastX, lastY + 1).IsWalkable && get(lastX, lastY + 1) == lastValue - 1) {
//                     path.Add(map.GetCell(lastX, lastY + 1));

//                     continue;
//                 }

//                 // Северо-восток
//                 if (map.GetCell(lastX + 1, lastY + 1).IsWalkable && get(lastX + 1, lastY + 1) == lastValue - 1) {                    
//                     path.Add(map.GetCell(lastX + 1, lastY + 1));

//                     continue;
//                 }

//                 // Восток
//                 if (map.GetCell(lastX + 1, lastY).IsWalkable && get(lastX + 1, lastY) == lastValue - 1) {             
//                     path.Add(map.GetCell(lastX + 1, lastY));

//                     continue;
//                 } 

//                 // Юго-восток
//                 if (map.GetCell(lastX + 1, lastY - 1).IsWalkable && get(lastX + 1, lastY - 1) == lastValue - 1) {                   
//                     path.Add(map.GetCell(lastX + 1, lastY - 1));

//                     continue;
//                 }

//                 // Юг
//                 if (map.GetCell(lastX, lastY - 1).IsWalkable && get(lastX, lastY - 1) == lastValue - 1) {                
//                     path.Add(map.GetCell(lastX, lastY - 1));

//                     continue;
//                 }

//                 // Юго-запад
//                 if (map.GetCell(lastX - 1, lastY - 1).IsWalkable && get(lastX - 1, lastY - 1) == lastValue - 1) {                   
//                     path.Add(map.GetCell(lastX - 1, lastY - 1));

//                     continue;
//                 }

//                 // Запад
//                 if (map.GetCell(lastX - 1, lastY).IsWalkable && get(lastX - 1, lastY) == lastValue - 1) {
//                     path.Add(map.GetCell(lastX - 1, lastY));

//                     continue;
//                 }

//                 // Северо-запад
//                 if (map.GetCell(lastX - 1, lastY + 1).IsWalkable && get(lastX - 1, lastY + 1) == lastValue - 1) {                  
//                     path.Add(map.GetCell(lastX - 1, lastY + 1));

//                     continue;
//                 }

//                 buildingPath = false;                                                                                                               
//             }

//             return path;
//         }

//         private int get(int cellX, int cellY) {
//             return graph[cellX, cellY];
//         }
//     }
// }