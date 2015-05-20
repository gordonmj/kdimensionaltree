using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace KTFormApp
{
    public class Node
    {
        private Point coord;
        private Color color;
        private int index = -1;
        public Node left = null;
        public Node right = null;
        public Node parent = null;
        public bool hasChildren = false;
        public bool isRoot = true;
        public int numRows = -1;
        public int numCols = -1;
        public int level = 0;

        public Node()
        {
            left = null;
            right = null;
            parent = null;
        }

        public Node(bool isItRoot)
        {
            isRoot = isItRoot;
            left = null;
            right = null;
            parent = null;
        }

        public Node(Point p)
        {
            coord = p;
            left = null;
            right = null;
            parent = null;
        }

        public Node(Color c)
        {
            color = c;
            left = null;
            right = null;
            parent = null;
        }

        public Node(Point p, Color c)
        {
            coord = p;
            color = c;
            left = null;
            right = null;
            parent = null;
        }

        public Node(Node prnt)
        {
            left = null;
            right = null;
            parent = prnt;
            prnt.hasChildren = true;
            hasChildren = false;
            isRoot = false;
            level = prnt.level + 1;
            numCols = prnt.numCols / 2;
            numRows = prnt.numRows / 2;
        }

        public Node(int i)
        {
            index = i;
        }

        public void setIndex(int i)
        {
            index = i;
        }

        public int getIndex()
        {
            return index;
        }
        public String toString()
        {
            return "Has children: " + hasChildren + ", color: " + getColorString() + ", is root: " + isRoot + ", level: " + level;
        }
        public void setColor(Color c)
        {
            color = c;
        }

        public Color getColor()
        {
            return color;
        }

        public String getColorString()
        {
            if (color == Color.White)
            {
                return "white";
            }
            else if (color == Color.Black)
            {
                return "black";
            }
            else
            {
                return "gray";
            }
        }
        public void setPoint(Point p)
        {
            coord = p;
        }

        public Point getPoint()
        {
            return coord;
        }

        private void validate(Node n){
            if (color == Color.Gray && (left.color == right.color))
            {
                throw new Exception("Bad node");
            }
        }

        public Node addChild(bool leftChild)
        {
            hasChildren = true;
            if (leftChild)
            {
                left = new Node(false);
                left.parent = this;
                left.level = level + 1;
                left.numRows = numRows / 2;
                left.numCols = numCols / 2;
                return left;
            }
            else
            {
                right = new Node(false);
                right.parent = this;
                right.level = level + 1;
                right.numRows = numRows / 2;
                right.numCols = numCols / 2;
                return right;
            }
        }

        public String getDirectionString()
        {
            if (this == this.parent.left)
            {
                return "left";
            }
            else
            {
                return "right";
            }
        }
        public void addChild(bool leftChild, Node n)
        {
            this.hasChildren = true;
            n.isRoot = false;
            n.parent = this;
            n.level = this.level+1;
            n.numCols = numCols / 2;
            n.numRows = numRows / 2;
            if (leftChild)
            {
                left = n;
            }
            else
            {
                right = n;
            }
        }

        public void addChildren()
        {
            this.hasChildren = true;

            addChild(true);
            addChild(false);
        }

        public void removeChildren()
        {
            left = null;
            right = null;
            hasChildren = false;
        }

        public void prune()
        {
            if (hasChildren)
            {
                if (left.getColor() == right.getColor())
                {
                    removeChildren();
                }
                else
                {
                    left.prune();
                    right.prune();
                }
            }
        }

        public void backSetDimensions(int r, int c)
        {
            numRows = r;
            numCols = c;
            if (hasChildren)
            {
                left.backSetDimensions(r / 2, c / 2);
                right.backSetDimensions(r / 2, c / 2);
            }
        }
    }
}