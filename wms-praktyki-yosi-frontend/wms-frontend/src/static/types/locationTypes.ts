export type productLocation = {
  id: number,
  position: string,
  quantity: number
}

export type locationToEdit = {
  position: string,
  quantity: number
}

export type locationToAdd = {
  position: string,
  quantity: number,
  productId: number
}
