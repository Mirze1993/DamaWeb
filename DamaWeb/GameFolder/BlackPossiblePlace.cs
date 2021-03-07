﻿using Model.UIGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.GameFolder
{
    public class BlackPossiblePlace: BasePossiblePlace
    {
        public BlackPossiblePlace(UIPlayGame uIPlayGame)
        {
            playGame = uIPlayGame;
            allCoordinates = new List<Coordinate>();
            allCoordinates.AddRange(uIPlayGame.WhiteCoordinate);
            allCoordinates.AddRange(uIPlayGame.BlackCoordinate);
        }


        public UICoordinate PossiblePlace(byte x, byte y)
        {
            UICoordinate uICoordinate = new UICoordinate();
            if (playGame.Move.AgainDum)
            {
                if (x == playGame.Move.NewX && y == playGame.Move.NewY)
                    return uICoordinate = EveryWhereDum(x, y);
                return uICoordinate;
            }
            uICoordinate = SimpleDum(x, y);
            if (uICoordinate.PossibleCoordinates.Count > 0) return uICoordinate;
            if (AnyStoneDum()) return uICoordinate;
            return SimpleMove(x, y);
        }

        UICoordinate SimpleMove(byte x, byte y)
        {
            UICoordinate ui = new UICoordinate();

            //right down
            if (Check(allCoordinates, (byte)(x + 1), (byte)(y - 1)) == Possison.no)
                ui.PossibleCoordinates.Add(new Coordinate((byte)(x + 1), (byte)(y - 1)));
            //left down  
            if (Check(allCoordinates, (byte)(x - 1), (byte)(y - 1)) == Possison.no)
                ui.PossibleCoordinates.Add(new Coordinate((byte)(x - 1), (byte)(y - 1)));

            return ui;
        }

        public UICoordinate SimpleDum(byte x, byte y)
        {
            UICoordinate uICoordinate = new UICoordinate();

            //right up
            if (Check(playGame.WhiteCoordinate, (byte)(x + 1), (byte)(y - 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x + 2), (byte)(y - 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x + 2), (byte)(y - 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x + 1), (byte)(y - 1)));
                }
            }
            //left up  
            if (Check(playGame.WhiteCoordinate, (byte)(x - 1), (byte)(y - 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x - 2), (byte)(y - 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x - 2), (byte)(y - 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x - 1), (byte)(y - 1)));
                }
            }
            return uICoordinate;

        }


        public UICoordinate EveryWhereDum(byte x, byte y)
        {
            UICoordinate uICoordinate = new UICoordinate();


            //right down
            if (Check(playGame.WhiteCoordinate, (byte)(x + 1), (byte)(y + 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x + 2), (byte)(y + 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x + 2), (byte)(y + 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x + 1), (byte)(y + 1)));
                }
            }

            //left down  
            if (Check(playGame.WhiteCoordinate, (byte)(x - 1), (byte)(y + 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x - 2), (byte)(y + 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x - 2), (byte)(y + 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x - 1), (byte)(y + 1)));
                }
            }
            //right up
            if (Check(playGame.WhiteCoordinate, (byte)(x + 1), (byte)(y - 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x + 2), (byte)(y - 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x + 2), (byte)(y - 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x + 1), (byte)(y - 1)));
                }
            }
            //left up  
            if (Check(playGame.WhiteCoordinate, (byte)(x - 1), (byte)(y - 1)) == Possison.yes)
            {
                if (Check(allCoordinates, (byte)(x - 2), (byte)(y - 2)) == Possison.no)
                {
                    uICoordinate.PossibleCoordinates.Add(new Coordinate((byte)(x - 2), (byte)(y - 2)));
                    uICoordinate.DumCoordinates.Add(new Coordinate((byte)(x - 1), (byte)(y - 1)));
                }
            }
            return uICoordinate;
        }

        public bool AnyStoneDum()
        {
            foreach (var item in playGame.BlackCoordinate)
            {
                if (item.Z == 0)
                    if (SimpleDum(item.X, item.Y).PossibleCoordinates.Count > 0) return true;
            }
            return false;
        }
    }
}
