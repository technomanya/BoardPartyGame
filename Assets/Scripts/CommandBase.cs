using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class CommandBase
{
    private bool _isExecuting = false;

    public bool isExecuting  => _isExecuting;

    public async void Execute()
    {
        _isExecuting = true;
        await AsyncExecution();
        _isExecuting = false;
    }

    protected abstract Task AsyncExecution();
}
