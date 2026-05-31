import { homeFeatures } from '../../data/homeContent'

export function FeaturesSection() {
  return (
    <section className="feature-section" id="features">
      <div className="section-heading">
        <p className="eyebrow">Core capabilities</p>
        <h2>Everything is organized around preparation, practice, and review.</h2>
      </div>

      <div className="feature-grid">
        {homeFeatures.map((feature) => (
          <article className="feature-card" key={feature.title}>
            <div className="feature-icon" />
            <h3>{feature.title}</h3>
            <p>{feature.description}</p>
          </article>
        ))}
      </div>
    </section>
  )
}
