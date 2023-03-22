export type productLocation = {
  id: number,
  magazineId: number,
  position: string,
  quantity: number
}

export type locationToEdit = {
  position: string,
  magazineId: number,
  quantity: number
}

export type locationToAdd = {
  position: string,
  magazineId: number,
  quantity: number,
  productId: number
}
