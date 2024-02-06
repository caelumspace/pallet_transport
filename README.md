# Pallet transport Microservice.

This is my Pallet transport microservice project. 

### Description

This microservice selects the nearest robot to a pallet using an API

The endpoint to this API, when run locally for instance, has the format
https://localhost:5001/api/GetClosest/{loadId}/{x}/{y}

where 
loadId is the ID of the pallet to be moved
x is the pallet x-coordinate
y is the pallet y-coordinate

the return payload is:

```
{
    robotId: 58,
    distanceToGoal: 49.9, //Indicates how far the robot is from the load which needs to be moved.
    batteryLevel: 30 //Indicates current battery level of the robot.
}
```

### Future Enhancements

1. The exercise choses appropriate robot for pick-up, but does not take into account delivery. 
2. Since the service does not track the pallet, it would be beneficial to track the pallets that each robot picks up/moves
3. Determine if chosen robot has sufficient battery power to deliver the pallet. If not, chose next closest with enough power, etc.
4. The exercise assumes all pallets have the same weight, which is unlikely. An algorithm to calculate power/weight for delivery would be necessary. 
