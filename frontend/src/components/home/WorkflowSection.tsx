import { homeWorkflow } from '../../data/homeContent'

export function WorkflowSection() {
  return (
    <section className="workflow-section" id="workflow">
      <div className="section-heading">
        <p className="eyebrow">How it works</p>
        <h2>A simple flow that stays usable even when the interview prep gets messy.</h2>
      </div>

      <div className="workflow-list">
        {homeWorkflow.map((step, index) => (
          <article className="workflow-card" key={step}>
            <span className="workflow-index">0{index + 1}</span>
            <p>{step}</p>
          </article>
        ))}
      </div>
    </section>
  )
}
