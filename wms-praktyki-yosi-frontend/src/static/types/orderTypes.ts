export type order={
    id : string,
    interval : number,
    nextOrder : string, 
    client : string,
    magazineId : number,
    orderItems:orderItem[]
}
export type orderToSend ={
    interval : number,
    client : string,
    magazineId : number,
    items?:orderSendItem[]
}
export type orderItem = {
    id : number,
    productName : string,
    arriving : boolean,
    quantity : number,
    tag? : string
}

export type returnedOrder= {
    id : string,
    interval : number,
    nextOrder : string, 
    client : string,
    magazineId : number,
    status : string, 
    message : string,
    items?:orderItem[]
}
export type returnedOrderWithProducts ={
    id : string,
    interval : number,
    nextOrder : string, 
    client : string,
    magazineId : number,
    status : string, 
    message : string,
    items?:sendItemInOrder[]
}

export type orderSendItem = {
    productName : string,
    arriving : boolean,
    quantity : number,
    tag? : string
}

export type updatedOrder ={
    interval : number,
    client : string,
    magazineId : number,
}

export type updatedItemInOrder={
    arriving : boolean,
    quantity : number,
    tag? : string
}
export type sendItemInOrder = {
    id : string,
    productName : string,
    arriving : boolean,
    quantity : number,
    tag? : string
}
