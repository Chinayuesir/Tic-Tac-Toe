using System;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 主数据结构，使用数组管理以网格形式组织起来的元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EasyGrid<T>
    {
        private T[,] mGrid;
        private int mWidth;
        private int mHeight;

        public int Width => mWidth;
        public int Height => mHeight;

        public EasyGrid(int width, int height)
        {
            mWidth = width;
            mHeight = height;
            mGrid = new T[width, height];
        }

        /// <summary>
        /// 用t填充
        /// </summary>
        /// <param name="t"></param>
        public void Fill(T t)
        {
            for (var x = 0; x < mWidth; x++)
            {
                for (var y = 0; y < mHeight; y++)
                {
                    mGrid[x, y] = t;
                }
            }
        }

        /// <summary>
        /// 使用委托填充
        /// </summary>
        /// <param name="onFill"></param>
        public void Fill(Func<int, int, T> onFill)
        {
            for (var x = 0; x < mWidth; x++)
            {
                for (var y = 0; y < mHeight; y++)
                {
                    mGrid[x, y] = onFill(x, y);
                }
            }
        }

        /// <summary>
        /// 重置网格大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="onAdd"></param>
        public void Resize(int width, int height, Func<int, int, T> onAdd)
        {
            var newGrid = new T[width, height];
            for (var x = 0; x < mWidth; x++)
            {
                for (var y = 0; y < mHeight; y++)
                {
                    newGrid[x, y] = mGrid[x, y];
                }

                // x addition
                for (var y = mHeight; y < height; y++)
                {
                    newGrid[x, y] = onAdd(x, y);
                }
            }

            for (var x = mWidth;  x < width; x++)
            {
                // y addition
                for (var y = 0; y < height; y++)
                {
                    newGrid[x, y] = onAdd(x, y);
                }
            }

            // 清空之前的
            Fill(default(T));

            mWidth = width;
            mHeight = height;
            mGrid = newGrid;
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="each"></param>
        public void ForEach(Action<int, int, T> each)
        {
            for (var x = 0; x < mWidth; x++)
            {
                for (var y = 0; y < mHeight; y++)
                {
                    each(x, y, mGrid[x, y]);
                }
            }
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="each"></param>
        public void ForEach(Action<T> each)
        {
            for (var x = 0; x < mWidth; x++)
            {
                for (var y = 0; y < mHeight; y++)
                {
                    each(mGrid[x, y]);
                }
            }
        }

        /// <summary>
        /// 重写索引器
        /// </summary>
        /// <param name="xIndex"></param>
        /// <param name="yIndex"></param>
        public T this[int xIndex, int yIndex]
        {
            get
            {
                if (xIndex >= 0 && xIndex < mWidth && yIndex >= 0 && yIndex < mHeight)
                {
                    return mGrid[xIndex, yIndex];
                }
                else
                {
                    Debug.LogWarning($"out of bounds [{xIndex}:{yIndex}] in grid[{mWidth}:{mHeight}]");
                    return default;
                }
            }
            set
            {
                if (xIndex >= 0 && xIndex < mWidth && yIndex >= 0 && yIndex < mHeight)
                {
                    mGrid[xIndex, yIndex] = value;
                }
                else
                {
                    Debug.LogWarning($"out of bounds [{xIndex}:{yIndex}] in grid[{mWidth}:{mHeight}]");
                }
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="cleanupItem"></param>
        public void Clear(Action<T> cleanupItem = null)
        {
            for (var x = 0; x < mWidth; x++)
            {
                for (var y = 0; y < mHeight; y++)
                {
                    cleanupItem?.Invoke(mGrid[x, y]);
                    mGrid[x, y] = default;
                }
            }
            mGrid = null;
        }
    }

}