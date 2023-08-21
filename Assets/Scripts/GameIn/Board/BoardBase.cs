using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class BoardBase<T> : IGenericBase<T> where T : IBlockEntity
{
    public T[] ActiveBlocks;
    public T GetBlock()
    {
        return default(T);
    }

    public T[] GetBlockElements(Expression<Func<T, bool>> predicate)
    {
        var pre = predicate.Compile();
        return ActiveBlocks.Where(pre).ToArray();
    }
}
