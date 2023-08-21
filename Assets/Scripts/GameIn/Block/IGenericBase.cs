using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public interface IGenericBase<T> where T: IBlockEntity
{
    T GetBlock();

    T[] GetBlockElements(Expression<Func<T, bool>> predicate);
}
