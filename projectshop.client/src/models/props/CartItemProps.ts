export interface CartItemProps {
  item: {
    id: string;
    name: string;
    category: 'food' | 'drink';
    price: number;
    quantity: number;
    image: string;
    description?: string;
  };
  onQuantityChange: (id: string, newQuantity: number) => void;
  onRemove: (id: string) => void;
}