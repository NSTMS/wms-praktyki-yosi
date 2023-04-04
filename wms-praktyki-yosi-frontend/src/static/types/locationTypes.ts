export type productLocation = {
  id: number;
  magazineId: number;
  position: string;
  quantity: number;
  tag : string
};

export type locationToEdit = {
  position: string;
  magazineId: number;
  quantity: number;
  tag : string
};

export type locationToAdd = {
  position: string;
  magazineId: number;
  quantity: number;
  productId: number;
  tag: string;
};
