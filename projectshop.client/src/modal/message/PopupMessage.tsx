import { useEffect, useRef } from 'react';
import type { MessageModalProps } from '../props/popup-message/message-modal-props';

const typeConfigs = {
  success: {
    icon: (
      <svg width="64" height="64" fill="none" viewBox="0 0 64 64">
        <circle cx="32" cy="32" r="30" stroke="#4ADE80" strokeWidth="4" fill="#ECFDF5" />
        <path d="M22 33L30 41L42 25" stroke="#22C55E" strokeWidth="4" strokeLinecap="round" />
      </svg>
    ),
    title: 'Thành công',
    color: '#22C55E'
  },
  error: {
    icon: (
      <svg width="64" height="64" fill="none" viewBox="0 0 64 64">
        <circle cx="32" cy="32" r="30" stroke="#F87171" strokeWidth="4" fill="#FEF2F2" />
        <line x1="24" y1="24" x2="40" y2="40" stroke="#EF4444" strokeWidth="4" strokeLinecap="round" />
        <line x1="40" y1="24" x2="24" y2="40" stroke="#EF4444" strokeWidth="4" strokeLinecap="round" />
      </svg>
    ),
    title: 'Lỗi',
    color: '#EF4444'
  },
  warning: {
    icon: (
      <svg width="64" height="64" fill="none" viewBox="0 0 64 64">
        <circle cx="32" cy="32" r="30" stroke="#FBBF24" strokeWidth="4" fill="#FFFBEB" />
        <rect x="30" y="18" width="4" height="20" rx="2" fill="#F59E42" />
        <circle cx="32" cy="44" r="2" fill="#F59E42" />
      </svg>
    ),
    title: 'Cảnh báo',
    color: '#F59E42'
  },
  info: {
    icon: (
      <svg width="64" height="64" fill="none" viewBox="0 0 64 64">
        <circle cx="32" cy="32" r="30" stroke="#38BDF8" strokeWidth="4" fill="#F0F9FF" />
        <rect x="30" y="24" width="4" height="16" rx="2" fill="#0EA5E9" />
        <circle cx="32" cy="44" r="2" fill="#0EA5E9" />
      </svg>
    ),
    title: 'Thông tin',
    color: '#0EA5E9'
  }
};

export function MessageModal({ message, type, timeout = 3000, marginTop = 0, onClose }: MessageModalProps) {
  const timerRef = useRef<NodeJS.Timeout | null>(null);

  useEffect(() => {
    timerRef.current = setTimeout(() => {
      if (onClose) onClose();
    }, timeout);
    return () => {
      if (timerRef.current) clearTimeout(timerRef.current);
    };
  }, [timeout, onClose]);

  const config = typeConfigs[type];

  return (
    <div
      style={{
        position: 'fixed',
        top: `${marginTop + 16}px`,
        right: '32px',
        zIndex: 9999,
        minWidth: 320,
        maxWidth: 380,
        background: '#fff',
        boxShadow: '0 4px 24px rgba(0,0,0,0.15)',
        borderRadius: 10,
        padding: '32px 24px',
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        animation: 'fade-in 0.2s',
        border: `2px solid ${config.color}`
      }}
      data-testid="message-modal"
    >
      <div style={{ marginBottom: 12 }}>{config.icon}</div>
      <div style={{ fontWeight: 600, fontSize: 22, color: config.color, marginBottom: 8 }}>
        {config.title}
      </div>
      <div style={{ fontSize: 16, color: '#444', textAlign: 'center' }}>
        {message}
      </div>
      <button
        aria-label="Đóng"
        style={{
          position: 'absolute',
          top: 14,
          right: 18,
          background: 'none',
          border: 'none',
          fontSize: 26,
          color: '#666',
          cursor: 'pointer'
        }}
        onClick={onClose}
      >
        ×
      </button>
      <style>{`
        @keyframes fade-in {
          from { opacity: 0; transform: translateY(-10px);}
          to { opacity: 1; transform: translateY(0);}
        }
      `}</style>
    </div>
  );
};