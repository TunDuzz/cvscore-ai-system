import type { PropsWithChildren } from 'react'

export function PublicLayout({ children }: PropsWithChildren) {
  return (
    <div className="site-shell">
      <div className="ambient ambient-left" />
      <div className="ambient ambient-right" />
      {children}
    </div>
  )
}
