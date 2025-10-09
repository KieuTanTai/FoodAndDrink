import { CircleIcon } from "lucide-react";
import type DotNavigationModalProps from "../../props/dots/DotNavigationModalProps";

function DotNavigationModal({ length, current, setCurrent, className = "" }: DotNavigationModalProps) {
  return (
    <div className={`flex gap-2 ${className}`}>
      {Array.from({ length }).map((_, idx) => (
        <CircleIcon
          key={idx}
          className={`w-4 h-4 rounded-full shadow-2xs ${current === idx ? "bg-blue-600 opacity-100" : "bg-gray-300 opacity-60 hover:opacity-100"} transition`}
          onClick={() => setCurrent(idx)} />
      ))}
    </div>
  );
};

export default DotNavigationModal;